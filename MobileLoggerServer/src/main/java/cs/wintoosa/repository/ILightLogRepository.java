/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.repository;

import cs.wintoosa.domain.LightLog;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author vkukkola
 */
public interface ILightLogRepository extends JpaRepository<LightLog, Long>, ILightLogRepositoryCustom{
    
}
