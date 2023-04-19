public class Herbivore : Animal
{
    private int hungerLevel;
    private const int MAX_HUNGER_LEVEL = 10;
    private int healthLevel;
    private const int MAX_HEALTH_LEVEL = 10;

    public Herbivore()
    {
        this.hungerLevel = MAX_HUNGER_LEVEL;
        this.healthLevel = MAX_HEALTH_LEVEL;
    }

    public void Eat(Rabbit rabbit){
    
    while(true){
    hungerLevel--;
    }
    
        if (this.hungerLevel >= 6)
        {
            rabbit.Eat(); 
            
        }
        
        while(this.hungerLevel == 0){
        healthLevel--;
        
        }
        else
        {
            return;
        }

        
    }
}
