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
public class CompLog extends Log{

    private Float trueHeading;
    
    private Float magneticHeading;
    
    private Float headingAccuracy;
    
    private Float rawMagneticReadingX;
    
    private Float rawMagneticReadingY;
    
    private Float rawMagneticReadingZ;

    public Float getTrueHeading() {
        return trueHeading;
    }

    public void setTrueHeading(Float trueHeading) {
        this.trueHeading = trueHeading;
    }

    public Float getMagneticHeading() {
        return magneticHeading;
    }

    public void setMagneticHeading(Float magneticHeading) {
        this.magneticHeading = magneticHeading;
    }

    public Float getHeadingAccuracy() {
        return headingAccuracy;
    }

    public void setHeadingAccuracy(Float headingAccuracy) {
        this.headingAccuracy = headingAccuracy;
    }

    public Float getRawMagneticReadingX() {
        return rawMagneticReadingX;
    }

    public void setRawMagneticReadingX(Float rawMagneticReadingX) {
        this.rawMagneticReadingX = rawMagneticReadingX;
    }

    public Float getRawMagneticReadingY() {
        return rawMagneticReadingY;
    }

    public void setRawMagneticReadingY(Float rawMagneticReadingY) {
        this.rawMagneticReadingY = rawMagneticReadingY;
    }

    public Float getRawMagneticReadingZ() {
        return rawMagneticReadingZ;
    }

    public void setRawMagneticReadingZ(Float rawMagneticReadingZ) {
        this.rawMagneticReadingZ = rawMagneticReadingZ;
    }
    
}
