public class Carnivore : Animal
{
    private int hungerLevel;
    private const int MAX_HUNGER_LEVEL = 10;
    private int healthLevel;
    private const int MAX_HEALTH_LEVEL = 10;

    public Carnivore()
    {
        this.hungerLevel = MAX_HUNGER_LEVEL;
        this.healthLevel = MAX_HEALTH_LEVEL;
    }

    public void Hunger(Fox fox)
    {
        if (this.hungerLevel < 6)
        {
          
            fox.hunt(); 
            this.hungerLevel++; 
        }
        
         if (this.hungerLevel == 0)
        {
           
            this.healthLevel--;
        }
        
        else
        {
           return;
        
        }

       
    }

