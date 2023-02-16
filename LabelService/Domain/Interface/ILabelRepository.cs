using LabelService.Domain.Models;

namespace LabelService.Domain.Interface
{
  public interface ILabelRepository
  {
    Task<int> CountLabels();
    Task<Label?> GetLabelById(int id);
    Task<IEnumerable<Label>> GetLabelsByName(string labelName);
    Task<Label?> GetLabelByTrackingId(string trackingId);
    Task<IEnumerable<Label>> GetLabels();
    Task<bool> InsertOrUpdate(Label label, int deviceId = 0);
  }
}