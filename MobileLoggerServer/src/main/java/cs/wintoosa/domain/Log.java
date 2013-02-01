package cs.wintoosa.domain;

import java.io.Serializable;
import javax.persistence.ElementCollection;
import javax.persistence.Embeddable;
import javax.persistence.EmbeddedId;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Inheritance;
import javax.persistence.InheritanceType;
import javax.persistence.ManyToMany;
import javax.persistence.ManyToOne;
import javax.persistence.Temporal;
import javax.validation.constraints.NotNull;
import org.hibernate.mapping.ForeignKey;
import org.jboss.logging.FormatWith;

/**
 *
 * @author jonimake
 */
@Entity
@Inheritance(strategy= InheritanceType.TABLE_PER_CLASS)
public class Log implements Serializable{
    private static final long serialVersionUID = 1234l;
    
    @Id
    @GeneratedValue(strategy= GenerationType.AUTO)
    private Long id;
    
    @NotNull
    private Long phoneId;
    
    @ManyToOne
    private Phone phone;
    
    @NotNull (message="timestamp must represent valid unixtime in seconds")
    private Long timestamp;
    
    /**
     * This is used for storing user search terms
     */
    private String text;
    
    public Log() {}
    
    public Log(long phoneId) {
        this.phoneId = phoneId;
    }
    
    public Long getPhoneId() {
        return phoneId;
    }

    public void setPhoneId(Long id) {
        this.phoneId = id;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long logId) {
        this.id = logId;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public Long getTimestamp() {
        return timestamp;
    }

    public void setTimestamp(Long timestamp) {
        this.timestamp = timestamp;
    }

    public Phone getPhone() {
        return phone;
    }

    public void setPhone(Phone phone) {
        this.phone = phone;
    }
}
