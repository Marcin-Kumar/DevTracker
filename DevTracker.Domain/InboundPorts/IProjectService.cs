using DevTracker.Domain.Entities;

namespace DevTracker.Domain.InboundPorts;
internal interface IProjectService
{
    public abstract List<Project> GetAllProjects();
    public abstract Project GetProjectById();
    public abstract void CreateProject();
    public abstract void DeleteProject();
    public abstract void UpdateProject();
}
