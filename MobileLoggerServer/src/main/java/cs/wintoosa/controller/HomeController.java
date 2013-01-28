package cs.wintoosa.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

/**
 *
 * @author jonimake
 */
@Controller 
public class HomeController {
    
    @RequestMapping(value = "/")
    public String index() {
        System.out.println("index");
        return "index";
    }
    
    
    
}
