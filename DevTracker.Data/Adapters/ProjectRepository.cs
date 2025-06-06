﻿using DevTracker.Core.Application.Exceptions;
using DevTracker.Core.Domain.Entities;
using DevTracker.Data.Context;
using DevTracker.Data.Mappers;
using DevTracker.Data.Models;
using DevTracker.Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Data.Adapters;

public class ProjectRepository : IProjectRepository
{
    private readonly DevTrackerContext _context;
    private readonly ProjectDataMapper _internalProjectMapper;

    public ProjectRepository(DevTrackerContext context, ProjectDataMapper internalProjectMapper)
    {
        _internalProjectMapper = internalProjectMapper;
        _context = context;
    }

    public async Task<ProjectEntity> CreateProject(ProjectEntity project)
    {
        Project projectModel = _internalProjectMapper.ToModel(project);
        await _context.Projects.AddAsync(projectModel);
        await _context.SaveChangesAsync();
        return _internalProjectMapper.ToEntity(projectModel);
    }

    public async Task<ProjectEntity> CreateProjectForGoalWithId(int goalId, ProjectEntity project)
    {
        Project projectModel = _internalProjectMapper.ToModel(project);
        projectModel.GoalId = goalId;
        await _context.Projects.AddAsync(projectModel);
        await _context.SaveChangesAsync();
        return _internalProjectMapper.ToEntity(projectModel);
    }

    public async Task DeleteProject(int id)
    {
        _context.Projects.Remove(new Project { Id = id , Title = "delete" });
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProjectEntity>> ReadAllProjects()
    {
        List<Project> projectModels = await _context.Projects.AsNoTracking().ToListAsync();
        return projectModels.ConvertAll(_internalProjectMapper.ToEntity);
    }

    public async Task<ProjectEntity> ReadProject(int id)
    {
        Project? project = await _context.Projects
                                    .Include(p => p.CodingSessions)
                                    .Include(p => p.TheorySessions)
                                    .Where(p => p.Id == id)
                                    .SingleOrDefaultAsync();
        if (project is null)
        {
            throw new NotFoundException($"Project with ID {id} not found.");
        }
        return _internalProjectMapper.ToEntity(project);
    }

    public async Task UpdateProject(ProjectEntity project)
    {
        Project projectModel = _internalProjectMapper.ToModel(project);
        _context.Projects.Update(projectModel);
        await _context.SaveChangesAsync();
    }
}