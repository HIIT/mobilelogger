package cs.wintoosa.repository;

import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author jonimake
 */
public interface IGenericRepository<T> extends JpaRepository<T, Long>, IGenericRepositoryCustom<T> {
    
}
