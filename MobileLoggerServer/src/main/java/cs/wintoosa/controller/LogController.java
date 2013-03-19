/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.domain.Log;
import cs.wintoosa.service.ILogService;
import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
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
    public List<Log> testGet() {
        System.out.println("test get");
        return logService.getAll();//currently returns system time in milliseconds
    }
    
    @RequestMapping(method= RequestMethod.POST)
    @ResponseBody
    public String testPost() {
        System.out.println("test post");
        return Long.toString(System.currentTimeMillis());
    }
    
}
