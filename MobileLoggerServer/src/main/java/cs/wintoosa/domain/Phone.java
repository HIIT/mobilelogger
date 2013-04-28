package cs.wintoosa.domain;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.OneToMany;

/**
 *
 * @author jonimake
 */
@Entity
public class Phone implements Serializable{
    
    @Id
    private String id;
    
    @OneToMany(targetEntity=SessionLog.class)
    private List<SessionLog> sessions = new ArrayList<SessionLog>();
    
    private static final long serialVersionUID = 9654699l;

    public List<SessionLog> getSessions() {
        return sessions;
    }

    public void setSessions(List<SessionLog> sessions) {
        this.sessions = sessions;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }
    
}
