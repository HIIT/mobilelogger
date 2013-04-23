package cs.wintoosa.controller;

import cs.wintoosa.service.ILogService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

/**
 *
 * @author jonimake
 */
@Controller 
public class HomeController {
    
    @Autowired
    ILogService logService;
    
    
    
    @RequestMapping(value = "/")
    public String index() {
        return "index";
    }
    
}
