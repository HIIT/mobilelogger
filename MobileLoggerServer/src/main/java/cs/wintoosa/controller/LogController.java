/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import com.google.gson.Gson;
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
    
    @RequestMapping(method= RequestMethod.POST)
    @ResponseBody
    public String testPost() {
        return Long.toString(System.currentTimeMillis());
    }
    
    
    @RequestMapping(value="/gps", method= RequestMethod.PUT, consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public boolean putPlainGps(@Valid @RequestBody Log log, BindingResult result) {
        if(result.hasErrors()) {
            System.out.println("result had errors");
            for(ObjectError error : result.getAllErrors())
                System.out.println(error.getObjectName());
            return false;
        }
        return logService.saveLog(log);
    }
    
    
    
    @RequestMapping(method= RequestMethod.PUT, consumes= MediaType.APPLICATION_JSON_VALUE )
    @ResponseBody
    public ServletResponse put(ServletResponse response) {
        System.out.println("PUT lolololo");
        System.out.println(response.toString());
        return response;
    }
    
    @RequestMapping(method= RequestMethod.GET)
    @ResponseBody
    public ServletResponse get(ServletResponse response) {
        System.out.println("GET lolololo");
        System.out.println(response.toString());
        return response;
    }
    
}
