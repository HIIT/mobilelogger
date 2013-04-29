/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import cs.wintoosa.domain.Sessionlog;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

/**
 * Currently this service interface defines only a method for formatting
 * data to be displayed on screen.
 * @author vkukkola
 */
public interface ISessionService {

    /**
     * Object which is used to transform data into a 2d array
     */
    class DataHolder {

        private Sessionlog session;
        private String phone;
        private Long sessionId;
        private Map<String, List<String>> logData;
        private List<Long> timestamps;

        public DataHolder(Sessionlog session){
            this.session = session;
            logData = new TreeMap<String, List<String>>();
            timestamps = new ArrayList<Long>();

            if (session != null) {
                for (Long i = (session.getSessionStart() / 10) * 10; i <= session.getSessionEnd(); i = i + 10) {
                    timestamps.add(i);
                }
                phone = session.getPhoneId();
                sessionId = session.getId();
            }
        }

        private void addColumn(String columnName) {
            logData.put(columnName, new ArrayList<String>());
        }

        public boolean hasColumn(String columnName) {
            return logData.containsKey(columnName);
        }

        public void addToColumn(String columnName, String data) {
            if (!hasColumn(columnName)) {
                addColumn(columnName);
            }
            List<String> column = logData.get(columnName);
            column.add(data);
        }

        public Map<String, List<String>> getLogData() {
            return logData;
        }

        public void setLogData(Map<String, List<String>> logData) {
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

        public List<Long> getTimestamps() {
            return timestamps;
        }

        public void setTimestamps(ArrayList<Long> timestamps) {
            this.timestamps = timestamps;
        }

        public Sessionlog getSession() {
            return session;
        }

        public void setSession(Sessionlog session) {
            this.session = session;
        }
    }

    public String getDataAsCsvString(Sessionlog session);
            
    public DataHolder formatForJsp(Sessionlog session);
}
