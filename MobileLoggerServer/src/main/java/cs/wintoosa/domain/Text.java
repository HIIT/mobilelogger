/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.domain;

import java.io.Serializable;
import javax.persistence.Entity;

/**
 *
 * @author vkukkola
 */
@Entity
public class Text extends Log implements Serializable{
    
    private String text;
    
    private String type;

    public void setText(String text) {
        this.text = text;
    }

    public String getText() {
        return text;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }
    
}
