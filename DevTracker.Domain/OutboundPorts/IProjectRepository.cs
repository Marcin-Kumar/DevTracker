using DevTracker.Domain.Entities;

namespace DevTracker.Domain.Ports;
public interface IProjectRepository
{
    public abstract void CreateProject(ProjectEntity project);
    public abstract void UpdateProject(ProjectEntity project);
    public abstract void DeleteProject(ProjectEntity id);
    public abstract void ReadAllProjects();
}
