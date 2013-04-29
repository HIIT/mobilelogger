package cs.wintoosa.domain;

import java.io.Serializable;
import java.util.List;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.OrderBy;
import javax.persistence.Transient;
import javax.validation.constraints.NotNull;

/**
 *
 * @author jonimake
 */
@Entity
public class Sessionlog implements Serializable{
    
    @GeneratedValue(strategy= GenerationType.AUTO)
    @Id
    private Long id;
    
    @NotNull
    private Long sessionStart;
    
    @NotNull
    private Long sessionEnd;
    
    @NotNull
    protected String phoneId;
    
    protected Long timestamp;
    
    @OneToMany(targetEntity=Abstractlog.class, mappedBy="sessionlog")
    @OrderBy("timestamp")                
    List<Abstractlog> logs;

    public void setLogs(List<Abstractlog> logs) {
        this.logs = logs;
    }

    public List<Abstractlog> getLogs() {
        return logs;
    }

    public void setChecksum(String checksum) {
        this.checksum = checksum;
    }

    public String getChecksum() {
        return checksum;
    }
    
    @Transient
    private String checksum;

    public Long getTimestamp() {
        return timestamp;
    }

    public String getPhoneId() {
        return phoneId;
    }

    public void setPhoneId(String phoneId) {
        this.phoneId = phoneId;
    }
        
    /*private Phone phone;

    public void setPhone(Phone phone) {
        this.phone = phone;
    }

    public Phone getPhone() {
        return phone;
    }
*/
    public Long getSessionStart() {
        return sessionStart;
    }

    public void setSessionStart(Long sessionStart) {
        this.sessionStart = sessionStart;
    }

    public Long getSessionEnd() {
        return sessionEnd;
    }

    public void setSessionEnd(Long sessionEnd) {
        this.sessionEnd = sessionEnd;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

}
