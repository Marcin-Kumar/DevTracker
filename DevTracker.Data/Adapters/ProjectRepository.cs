using DevTracker.Data.Context;
using DevTracker.Data.Mappers;
using DevTracker.Data.Models;
using DevTracker.Domain.Entities;
using DevTracker.Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Data.Adapters;

public class ProjectRepository : IProjectRepository
{
    private readonly DevTrackerContext _context;
    private readonly ProjectMapper _internalProjectMapper;

    public ProjectRepository(DevTrackerContext context, ProjectMapper internalProjectMapper)
    {
        _internalProjectMapper = internalProjectMapper;
        _context = context;
    }

    public async Task CreateProject(ProjectEntity project)
    {
        Project projectModel = _internalProjectMapper.ToModel(project);
        await _context.Projects.AddAsync(projectModel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProject(int id)
    {
        _context.Projects.Remove(new Project { Id = id });
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProjectEntity>> ReadAllProjects()
    {
        List<Project> projectModels = await _context.Projects.AsNoTracking().ToListAsync();
        return projectModels.Select(_internalProjectMapper.ToEntity).ToList();
    }

    public async Task<ProjectEntity> ReadProject(int id)
    {
        Project project = await _context.Projects
                                    .Include(p => p.CodingSessions)
                                    .Include(p => p.TheorySessions)
                                    .AsNoTracking()
                                    .Where(p => p.Id == id)
                                    .SingleOrDefaultAsync();
        return _internalProjectMapper.ToEntity(project);
    }

    public async Task UpdateProject(ProjectEntity project)
    {
        Project projectModel = _internalProjectMapper.ToModel(project);
        _context.Projects.Update(projectModel);
        await _context.SaveChangesAsync();
    }
}