package cs.wintoosa.domain;

import javax.persistence.Entity;

/**
 *
 * @author jonimake
 */
@Entity
public class Weather extends Abstractlog{
    /**
     * Temperature in degrees C
     */
    private int temperature;
    
    /**
     * Windspeed in km/h
     */
    private int windspeed;
    
    
    private String weatherdescription;  

    /**
     * @return temperature in degrees C
     */
    public int getTemperature() {
        return temperature;
    }

    /**
     * @param temperature in degrees C 
     */
    public void setTemperature(int temperature) {
        this.temperature = temperature;
    }

    /**
     * @return windspeed in km/h
     */
    public int getWindspeed() {
        return windspeed;
    }

    /**
     * @param windspeed in km/h
     */
    public void setWindspeed(int windspeed) {
        this.windspeed = windspeed;
    }

    public String getWeatherdescription() {
        return weatherdescription;
    }

    public void setWeatherdescription(String weatherdescription) {
        this.weatherdescription = weatherdescription;
    }
}
