
public class PreparationStageStartEvent : CustomEvent
{
    public float preparationTime;

    public PreparationStageStartEvent(float preparationTime) : base("This is a Preparation Stages has Started Event")
    {
        this.preparationTime = preparationTime;
    }
}
