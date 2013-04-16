/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.TextLog;
import cs.wintoosa.service.ILogService;
import java.util.List;
import javax.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Controller;
import org.springframework.validation.BindingResult;
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
    
    @RequestMapping(method=RequestMethod.PUT, consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public boolean putLog(@Valid @RequestBody TextLog data, BindingResult result) {
        System.out.println("at Google controller, log is: "+data);
        data.setType("search");
        return logService.saveLog(data);
    }
    
    @RequestMapping(method=RequestMethod.GET)
    @ResponseBody
    public List<Log> getLogs(){
        return logService.getAll(TextLog.class);
    }
}
