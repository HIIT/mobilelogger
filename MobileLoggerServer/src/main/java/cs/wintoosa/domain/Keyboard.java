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
public class Keyboard extends Log {
    
    private String keyboardFocus;

    public void setKeyboardFocus(String keyboardFocus) {
        this.keyboardFocus = keyboardFocus;
    }

    public String getKeyboardFocus() {
        return keyboardFocus;
    }
    
}
