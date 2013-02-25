/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.domain.GpsLog;
import cs.wintoosa.domain.Log;
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
@RequestMapping(value = "/log/gps")
public class GPSController {
    
    @Autowired
    ILogService logService;
    
    @RequestMapping(method= RequestMethod.GET)
    @ResponseBody
    public List<GpsLog> getLogs() {
        System.out.println("printing logs");
        return logService.getGpsLogs(); //Currently returns all logs, not only gps logs
        //return "Under Contsrtuction!";
    }
    
    @RequestMapping(method= RequestMethod.PUT, consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public boolean putGPSLog(@Valid @RequestBody GpsLog log, BindingResult result) {
        System.out.println("put plain log");
        if(result.hasErrors()) {
            System.out.println("result had errors");
            for(ObjectError error : result.getAllErrors())
                System.out.println(error.toString());
            return false;
        }
        return logService.saveLog(log);
    }
    
    @RequestMapping(value="/put", method=RequestMethod.GET)
    public String putDummyLog(){
        GpsLog log = new GpsLog();
        log.setAlt(1f);
        log.setLat(2f);
        log.setLon(2f);
        log.setPhoneId("13245687890");
        log.setTimestamp(System.currentTimeMillis());
        logService.saveLog(log);
        System.out.println("Added dumy log");
        return "redirect:/";
    }
}
