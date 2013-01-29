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
import javax.validation.constraints.NotNull;
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
    @NotNull
    private Long phoneId;
    
    @NotNull
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

    public Long getPhoneId() {
        return phoneId;
    }

    public void setPhoneId(Long id) {
        this.phoneId = id;
    }
}
