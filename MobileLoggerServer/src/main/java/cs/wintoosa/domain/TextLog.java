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
public class TextLog extends Log{
    
    private String text;

    public void setText(String text) {
        this.text = text;
    }

    public String getText() {
        return text;
    }
    
}
