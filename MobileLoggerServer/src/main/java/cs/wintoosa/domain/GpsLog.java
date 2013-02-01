package cs.wintoosa.domain;

import java.io.Serializable;
import javax.persistence.Entity;
import javax.validation.constraints.NotNull;

/**
 *
 * @author jonimake
 */
@Entity
public class GpsLog extends Log{
    @NotNull(message="Latitude must not be null")
    private Float lat;
    @NotNull(message="Lognitude must not be null")
    private Float lon;
    @NotNull(message="Altitude must not be null")
    private float alt;
    

    public Float getLat() {
        return lat;
    }

    public void setLat(Float lat) {
        this.lat = lat;
    }

    public Float getLon() {
        return lon;
    }

    public void setLon(Float lon) {
        this.lon = lon;
    }

    public float getAlt() {
        return alt;
    }

    public void setAlt(float alt) {
        this.alt = alt;
    }

    
}
