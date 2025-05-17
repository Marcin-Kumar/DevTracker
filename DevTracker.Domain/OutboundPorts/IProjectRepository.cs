using DevTracker.Domain.Entities;

namespace DevTracker.Domain.Ports;
public interface IProjectRepository
{
    public abstract Task CreateProject(ProjectEntity project);
    public abstract Task UpdateProject(ProjectEntity project);
    public abstract Task DeleteProject(int id);
    public abstract Task<List<ProjectEntity>> ReadAllProjects();
}
