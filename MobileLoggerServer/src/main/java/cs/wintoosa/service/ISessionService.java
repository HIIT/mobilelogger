/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import cs.wintoosa.domain.SessionLog;
import java.util.HashMap;
import java.util.Map;

/**
 *
 * @author vkukkola
 */
public interface ISessionService {
    
    class DataHolder{
        private String phone;
        private Long sessionId;
        private Map<String, Map<Long, String>> logData;
        public DataHolder(SessionLog session){
            logData = new HashMap<String, Map<Long, String>>();
            if(session != null){
                phone = session.getPhoneId();
                sessionId = session.getId();
            }
        }
        
        private void addColumn(String columnName){
            logData.put(columnName, new HashMap<Long, String>());
        }
        
        public boolean hasColumn(String columnName){
            return logData.containsKey(columnName);
        }
        
        public void addToColumn(String columnName, Long id, String data){
            if(!hasColumn(columnName)){
                addColumn(columnName);
            }
            Map<Long, String> column = logData.get(columnName);
            column.put(id, data);
        }

        public Map<String, Map<Long, String>> getLogData() {
            return logData;
        }

        public void setLogData(Map<String, Map<Long, String>> logData) {
            this.logData = logData;
        }

        public String getPhone() {
            return phone;
        }

        public void setPhone(String phone) {
            this.phone = phone;
        }

        public Long getSessionId() {
            return sessionId;
        }

        public void setSessionId(Long sessionId) {
            this.sessionId = sessionId;
        }
    }
    
    public DataHolder formatForJsp(SessionLog session);
    
}
