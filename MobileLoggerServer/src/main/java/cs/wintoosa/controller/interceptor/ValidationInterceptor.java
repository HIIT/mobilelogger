
package cs.wintoosa.controller.interceptor;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.springframework.web.servlet.ModelAndView;
import org.springframework.web.servlet.handler.HandlerInterceptorAdapter;
import java.util.logging.*;

/**
 *
 * @author jonimake
 */
public class ValidationInterceptor extends HandlerInterceptorAdapter {
    
    private final static Logger logger = Logger.getLogger(ValidationInterceptor.class.getName()); 
              
    @Override
    public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler) throws Exception {

        
        System.out.println(request.getContextPath());
        System.out.println(request.getMethod());
        System.out.println(request.getQueryString());
        
        
        logger.info(request.getContextPath());
        logger.info(request.getMethod());
        logger.info(request.getQueryString());
        
        
        return super.preHandle(request, response, handler);
    }

    @Override
    public void postHandle(HttpServletRequest request, HttpServletResponse response, Object handler, ModelAndView modelAndView) throws Exception {
        System.out.println("postHandle");
        System.out.println(request.toString());
        super.postHandle(request, response, handler, modelAndView);
    }
    
    
    
}
