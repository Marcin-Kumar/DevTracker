using DevTracker.Domain.Entities;
using DevTracker.Domain.InboundPorts;
using DevTracker.Domain.Ports;

namespace DevTracker.Domain.Application;
internal class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectEntity> CreateProject(ProjectEntity projectEntity)
    {
        return await _projectRepository.CreateProject(projectEntity);
    }

    public async Task<ProjectEntity> CreateProjectForGoalWithId(int goalId, ProjectEntity projectEntity)
    {
        return await _projectRepository.CreateProjectForGoalWithId(goalId, projectEntity);
    }

    public async Task DeleteProject(int id)
    {
        await _projectRepository.DeleteProject(id);
    }

    public async Task<List<ProjectEntity>> ReadAllProjects()
    {
        return await _projectRepository.ReadAllProjects();
    }

    public Task<ProjectEntity> ReadProjectById(int id)
    {
        return _projectRepository.ReadProject(id);
    }

    public async Task UpdateProject(ProjectEntity projectEntity)
    {
       await _projectRepository.UpdateProject(projectEntity);
    }
}
