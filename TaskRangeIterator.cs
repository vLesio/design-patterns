public class TaskRangeIterator
{
    private WorkManager workManager;
    private string currentRange;
    private List<string> availableRanges;
    private List<string> reservedRanges;

    public TaskRangeIterator(WorkManager workManager)
    {
        this.workManager = workManager;
        availableRanges = new List<string>();
        reservedRanges = new List<string>();
    }

    public string GetNext()
    {
        // Logic to return the next available range
        // Update availableRanges and reservedRanges accordingly
        return "nextRange";
    }

    public void Update()
    {
        // Logic to update available and reserved ranges
    }
}
