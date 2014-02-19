
namespace DTTraceFileHelper
{
    internal interface ITraceResultBuilder
    {
        string GetTraceResultBlock(IAddressProvider addressProvider);
        TraceResultOverview GetTraceResultOverview();
    }
}
