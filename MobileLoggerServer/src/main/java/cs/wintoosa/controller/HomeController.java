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
        System.out.println("index");
        
        return "index";
    }
    
    /*@RequestMapping(value="/demoview", produces=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public List<Log> demoView() {
        List<Log> demolist = new ArrayList();
        Log log = new Log();
        log.setPhoneId(Long.MIN_VALUE+"");
        log.setTimestamp(Long.MAX_VALUE);
        
        
        
        GpsLog gpslog = new GpsLog();
        gpslog.setLat(50.5f);
        gpslog.setLon(66f);
        gpslog.setAlt(120f);
        gpslog.setPhoneId(Long.MIN_VALUE+"");
        gpslog.setTimestamp(Long.MIN_VALUE);

        logService.saveLog(log);
        logService.saveLog(gpslog);
        
        
        
        return logService.getAll();
    }*/
    
    
    
}
