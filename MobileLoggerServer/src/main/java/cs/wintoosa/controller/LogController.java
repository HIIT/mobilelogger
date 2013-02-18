/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import com.google.gson.Gson;
import cs.wintoosa.domain.GpsLog;
import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.Phone;
import cs.wintoosa.service.ILogService;
import cs.wintoosa.service.LogService;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import javax.activation.MimeType;
import javax.servlet.ServletResponse;
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
 * @author jonimake
 */
@Controller
@RequestMapping(value = "/log")
public class LogController {
    
    @Autowired
    ILogService logService;
    
    
    @RequestMapping(method= RequestMethod.GET)
    @ResponseBody
    public String testGet() {
        System.out.println("test get");
        return Long.toString(System.currentTimeMillis());
    }
    
    @RequestMapping(method= RequestMethod.POST)
    @ResponseBody
    public String testPost() {
        System.out.println("test post");
        return Long.toString(System.currentTimeMillis());
    }
    
    @RequestMapping(method= RequestMethod.PUT, consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public boolean putPlainLog(@Valid @RequestBody Log log, BindingResult result) {
        System.out.println("put plain log");
        if(result.hasErrors()) {
            System.out.println("result had errors");
            for(ObjectError error : result.getAllErrors())
                System.out.println(error.toString());
            return false;
        }
        return logService.saveLog(log);
    }
    
    
    @RequestMapping(value="/gps", method= RequestMethod.PUT, consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public boolean putPlainGps(@Valid @RequestBody GpsLog log, BindingResult result) {
        System.out.println("put gps");
        if(result.hasErrors()) {
            System.out.println("result had errors");
            for(ObjectError error : result.getAllErrors())
                System.out.println(error.toString());
            return false;
        }
        return logService.saveLog(log);
    }
}
