using LabelService.Domain.Models;

namespace LabelService.Domain.Interface
{
    public interface ILabelRepository
    {
        Task<int> CountLabels();
        Task<Label?> GetLabelById(int id);
        Task<IEnumerable<Label>> GetLabels();
    }
}