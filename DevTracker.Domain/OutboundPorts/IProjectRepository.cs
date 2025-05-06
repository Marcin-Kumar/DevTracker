using DevTracker.Domain.Entities;

namespace DevTracker.Domain.Ports;
public interface IProjectRepository
{
    public abstract void CreateProject(Project project);
    public abstract void UpdateProject(Project project);
    public abstract void DeleteProject(Project id);
    public abstract void ReadAllProjects();
}
