namespace CourseWork_2.Domain.Models;

public class RewardHistory
{
    private List<Reward> _rewards;

    public List<Reward> Rewards => _rewards;

    public RewardHistory(List<Reward> rewards)
    {
        _rewards = rewards;
    }

    public void AddReward(Reward reward)
    {
        _rewards.Add(reward);
    }
}