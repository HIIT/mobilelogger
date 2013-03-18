package cs.wintoosa.domain;

import java.io.Serializable;
import java.util.LinkedList;
import java.util.List;
import javax.persistence.CollectionTable;
import javax.persistence.ElementCollection;
import javax.persistence.Embedded;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.OneToMany;
import javax.validation.constraints.NotNull;

/**
 *
 * @author jonimake
 */
@Entity
@Deprecated
public class Phone implements Serializable{
    
    private static final long serialVersionUID = 999l;
    
    @Id
    private Long imei;
    

    //@OneToMany
    //private List<Log> logs = new LinkedList();
    
    public Long getImei() {
        return imei;
    }

    public void setImei(Long id) {
        this.imei = id;
    }
/*
    public List<Log> getLogs() {
        return logs;
    }

    public void setLogs(List<Log> logs) {
        this.logs = logs;
    }

    public void addLog(Log log) {
        this.logs.add(log);
    }*/
}
