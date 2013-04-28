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
    
    //@JsonIgnore
    private String text;

    private String type;

    //@JsonIgnore
    public void setText(String text) {
        this.text = text;
    }

   // @JsonIgnore
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
