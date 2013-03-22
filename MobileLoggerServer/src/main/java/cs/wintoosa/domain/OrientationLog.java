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
public class OrientationLog extends Log{

    private Float currentRotationRateX;
    
    private Float currentRotationRateY;
    
    private Float currentRotationRateZ;

    public Float getCurrentRotationRateX() {
        return currentRotationRateX;
    }

    public void setCurrentRotationRateX(Float currentRotationRateX) {
        this.currentRotationRateX = currentRotationRateX;
    }

    public Float getCurrentRotationRateY() {
        return currentRotationRateY;
    }

    public void setCurrentRotationRateY(Float currentRotationRateY) {
        this.currentRotationRateY = currentRotationRateY;
    }

    public Float getCurrentRotationRateZ() {
        return currentRotationRateZ;
    }

    public void setCurrentRotationRateZ(Float currentRotationRateZ) {
        this.currentRotationRateZ = currentRotationRateZ;
    }
}
