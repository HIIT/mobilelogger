/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.domain.TextLog;
import cs.wintoosa.service.ILogService;
import java.util.List;
import javax.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Controller;
import org.springframework.validation.BindingResult;
import org.springframework.validation.ObjectError;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

/**
 *
 * @author vkukkola
 */
@Controller
@RequestMapping(value = "/log/clicked")
public class SearchClickedController {
    
    @Autowired
    ILogService logService;
    
    @RequestMapping(method= RequestMethod.GET)
    @ResponseBody
    public List<TextLog> getLogs() {
        System.out.println("printing logs");
        return logService.getTextLogs(); //Currently returns all logs, not only gps logs
        //return "Under Contsrtuction!";
    }
    
    @RequestMapping(method= RequestMethod.PUT, consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public boolean putGPSLog(@Valid @RequestBody TextLog log, BindingResult result) {
        System.out.println("put plain log");
        if(result.hasErrors()) {
            System.out.println("result had errors");
            for(ObjectError error : result.getAllErrors())
                System.out.println(error.toString());
            return false;
        }
        return logService.saveLog(log);
    }
    
}
