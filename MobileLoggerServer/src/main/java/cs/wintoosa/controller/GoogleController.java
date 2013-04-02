/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.domain.TextLog;
import cs.wintoosa.service.ILogService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

/**
 *
 * @author jonimake
 */
@Controller
@RequestMapping(value="/log/google")
public class GoogleController {
    @Autowired
    ILogService logService;
    
    @RequestMapping(method=RequestMethod.PUT)
    @ResponseBody
    public boolean putLog(@RequestBody TextLog data) {
        return logService.saveLog(data);
    }
    
    

    public void setLogService(ILogService logService) {
        this.logService = logService;
    }
}
