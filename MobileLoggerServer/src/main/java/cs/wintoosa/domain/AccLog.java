/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.domain;

import javax.persistence.Entity;

/**
 *
 * @author vkukkola
 */
@Entity
public class AccLog extends Log{

    private Float X;
    
    private Float Y;
    
    private Float Z;

    public Float getX() {
        return X;
    }

    public void setX(Float X) {
        this.X = X;
    }

    public Float getY() {
        return Y;
    }

    public void setY(Float Y) {
        this.Y = Y;
    }

    public Float getZ() {
        return Z;
    }

    public void setZ(Float Z) {
        this.Z = Z;
    }
    
}
