package cs.wintoosa.domain;

import java.io.Serializable;
import javax.persistence.ElementCollection;
import javax.persistence.Embeddable;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.ManyToMany;
import javax.persistence.ManyToOne;
import org.hibernate.mapping.ForeignKey;
import org.jboss.logging.FormatWith;

/**
 *
 * @author jonimake
 */
@Entity
public class Log implements Serializable{
    private static final long serialVersionUID = 1234l;
    
    @Id
    @GeneratedValue(strategy= GenerationType.AUTO)
    private Long id;
    
    private String text;
    
    public Log() {
        
    }
    
    public Log(String logText) {
        this.text = logText;
    }
    
    public String getText() {
        return text;
    }

    public void setText(String lines) {
        this.text = lines;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }
}
