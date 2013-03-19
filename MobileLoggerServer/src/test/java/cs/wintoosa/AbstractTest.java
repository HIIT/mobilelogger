package cs.wintoosa;

import java.lang.annotation.Inherited;
import org.junit.runner.RunWith;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

/**
 * Just an abstract class that holds the context configurations
 * @author jonimake
 */

@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(locations =
{
    "classpath:spring-context-test.xml",
    "classpath:spring-database-test.xml"
})
public abstract class AbstractTest {
    
}
