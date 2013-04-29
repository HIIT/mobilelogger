/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.domain;

import javax.persistence.Entity;

/**
 *
 * @author jonimake
 */
@Entity
public class KeyPress extends Abstractlog{
    
    private String keyPressed;

    public void setKeyPressed(String keyPressed) {
        this.keyPressed = keyPressed;
    }

    public String getKeyPressed() {
        return keyPressed;
    }
}
