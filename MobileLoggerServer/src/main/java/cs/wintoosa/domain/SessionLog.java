package cs.wintoosa.domain;

import java.util.List;
import javax.persistence.Entity;
import javax.persistence.ManyToMany;
import javax.persistence.OneToMany;

/**
 *
 * @author jonimake
 */
@Entity
public class SessionLog extends Log{
    private Long sessionStart;
    
    private Long sessionEnd;
    
    @OneToMany
    private List<Log> logs;

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

    public List<Log> getLogs() {
        return logs;
    }

    public void setLogs(List<Log> logs) {
        this.logs = logs;
    }

}
