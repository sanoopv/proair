namespace ProAir
{
    public interface ISeatBank
    {
        bool[] Seats { get; set; }
        int Vacancy();
    }
}