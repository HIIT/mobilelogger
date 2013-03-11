/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.repository;

import cs.wintoosa.domain.ProximityLog;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author vkukkola
 */
public interface IProximityLogRepository extends JpaRepository<ProximityLog, Long>, IProximityLogRepositoryCustom{
    
}
