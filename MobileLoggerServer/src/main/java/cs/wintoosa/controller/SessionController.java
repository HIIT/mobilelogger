/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.domain.SessionLog;
import cs.wintoosa.service.ILogService;
import java.util.List;
import javax.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Controller;
import org.springframework.validation.BindingResult;
import org.springframework.validation.ObjectError;
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
    
    @RequestMapping(method= RequestMethod.PUT, consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public SessionLog putSessionLog(@Valid @RequestBody SessionLog log, BindingResult result) {
        if(result.hasErrors()) {
            for(ObjectError error : result.getAllErrors())
                System.out.println(error.toString());
            return null;
        }
        return logService.saveSessionLog(log);
    }
    
    @RequestMapping(method= RequestMethod.GET)
    @ResponseBody
    public List<SessionLog> getLogs() {
        return logService.getAllSessions();
    }
    
    @RequestMapping(value = "{sessionId}", method = RequestMethod.GET)
    @ResponseBody
    public SessionLog getLogsBySession(@PathVariable("sessionId") long sessionId) {
        return logService.getSessionById(sessionId);
    }
    
}
