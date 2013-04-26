/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.dataformat.csv.CsvMapper;
import com.fasterxml.jackson.dataformat.csv.CsvSchema;
import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.SessionLog;
import cs.wintoosa.service.ILogService;
import cs.wintoosa.service.ISessionService;
import java.util.List;
import java.util.Map;
import javax.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.validation.ObjectError;
import org.springframework.web.bind.WebDataBinder;
import org.springframework.web.bind.annotation.InitBinder;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

/**
 *
 * @author jonimake
 */
@Controller
@RequestMapping("/log/session")
public class SessionController {
    
    @Autowired
    ILogService logService;
    
    @Autowired
    ISessionService sessionService;
    
    @RequestMapping(method= RequestMethod.PUT, consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public SessionLog putSessionLog(@Valid @RequestBody SessionLog log) {
        return logService.saveSessionLog(log);
    }
    
    @RequestMapping(method= RequestMethod.GET)
    @ResponseBody
    public List<SessionLog> getLogs() {
        return logService.getAllSessions();
    }
    
    @RequestMapping(value = "/{sessionId}", method = RequestMethod.GET)
    @ResponseBody
    public List<Log> getLogsBySession(@PathVariable long sessionId, Model model) {
        System.out.println("getting logs in session");
        return logService.getAllBySessionId(logService.getSessionById(sessionId));
    }
    
    @RequestMapping(value = "/{sessionId}/matrix", method = RequestMethod.GET)
    public String getLogsBySessionMatrix(@PathVariable long sessionId, Model model) {
        System.out.println("getting logs in session");
        model.addAttribute("session", sessionService.formatForJsp(logService.getSessionById(sessionId)));
        return "matrix";
    }
    
    @RequestMapping(value = "/{sessionId}/csv", method = RequestMethod.GET)
    @ResponseBody
    public String getLogsBySessionCsv(@PathVariable long sessionId, Model model) throws JsonProcessingException {
        System.out.println("getting logs in session");
        sessionService.getDataAsCsvString(logService.getSessionById(sessionId));
        return null;
    }
   
    
    //Adds dummy logs to DB for debugging
    @RequestMapping(value ="/add", method=RequestMethod.GET)
    public String seedLogs(){
        SessionLog log = new SessionLog();
        log.setSessionStart(new Long(0));
        log.setSessionEnd(new Long(100));
        log.setPhoneId("foo");
        logService.saveSessionLog(log);
        return "redirect:/log/gps/put";
    }
    
}
