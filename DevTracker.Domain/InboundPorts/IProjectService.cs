using DevTracker.Domain.Entities;

namespace DevTracker.Domain.InboundPorts;
internal interface IProjectService
{
    public abstract List<ProjectEntity> GetAllProjects();
    public abstract ProjectEntity GetProjectById();
    public abstract void CreateProject();
    public abstract void DeleteProject();
    public abstract void UpdateProject();
}
