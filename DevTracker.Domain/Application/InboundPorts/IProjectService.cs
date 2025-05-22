using DevTracker.Core.Domain.Entities;

namespace DevTracker.Core.Application.InboundPorts;
public interface IProjectService
{
    public abstract Task<ProjectEntity> CreateProject(ProjectEntity projectEntity);
    public abstract Task<ProjectEntity> CreateProjectForGoalWithId(int goalId, ProjectEntity projectEntity);
    public abstract Task<List<ProjectEntity>> ReadAllProjects();
    public abstract Task<ProjectEntity> ReadProjectById(int id);
    public abstract Task UpdateProject(ProjectEntity projectEntity);
    public abstract Task DeleteProject(int id);
}
