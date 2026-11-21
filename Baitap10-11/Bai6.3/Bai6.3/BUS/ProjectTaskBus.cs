using Bai6._3.DAL;
using Bai6._3.DTO;

namespace Bai6._3.BUS;

public sealed class ProjectTaskBus
{
    private readonly ProjectTaskDal dal = new();
    public void EnsureConnection() => dal.TestConnection();
    public void EnsureSampleMembers() => dal.EnsureSampleMembers();
    public List<ProjectDto> GetProjects() => dal.GetProjects();
    public List<MemberDto> GetMembers() => dal.GetMembers();
    public List<TaskDto> GetTasks(int? projectId, string? status) => dal.GetTasks(projectId, status);
    public List<TaskDto> GetOverdueTasks() => dal.GetOverdueTasks();
    public List<CompletionReportDto> GetCompletionReport() => dal.GetCompletionReport();
    public void AddProject(ProjectDto project) => dal.AddProject(project);
    public void UpdateProject(int projectId, string name) => dal.UpdateProject(projectId, name);
    public void DeleteProject(int projectId) => dal.DeleteProject(projectId);
    public void AddTask(TaskDto task, IEnumerable<int> memberIds) => dal.AddTask(task, memberIds);
    public void UpdateTaskStatus(int taskId, string status) => dal.UpdateTaskStatus(taskId, status);
}
