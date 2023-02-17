using Grpc.Core;

namespace InternalGrpcService.Services
{
  public class LabelService : Greeter.GreeterBase
  {
    public override async Task<LabelResponse> GetLabels(LabelRequest request, ServerCallContext context)
    {
      var rs = new LabelResponse();
      //rs.Labels.Add()
      return rs;
    }
  }
}
