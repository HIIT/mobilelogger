/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import cs.wintoosa.domain.SessionLog;
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
                if(true){
                    
                }
                //data.addToColumn("foo", log.getTimestamp(), "bar");
            
        }
        return data;
    }
    
    private void pullFromDb(){
        
    }
}
