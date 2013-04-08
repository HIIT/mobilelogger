/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.SessionLog;
import java.util.List;
import org.springframework.stereotype.Service;


/**
 *
 * @author vkukkola
 */
@Service
public class SessionService implements ISessionService {

    @Override
    public DataHolder formatForJsp(SessionLog session) {
        DataHolder data = new DataHolder(session);
        if(session != null){
            List<Log> logs = session.getLogs();
            for(Log log : logs){
                data.addToColumn("foo", log.getTimestamp(), "bar");
            }
        }
        return data;
    }
}
