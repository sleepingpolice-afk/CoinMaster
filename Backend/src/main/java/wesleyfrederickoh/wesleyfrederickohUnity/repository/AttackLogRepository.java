package wesleyfrederickoh.wesleyfrederickohUnity.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import wesleyfrederickoh.wesleyfrederickohUnity.model.AttackLog;

import java.util.UUID;

@Repository
public interface AttackLogRepository extends JpaRepository<AttackLog, UUID> {
}
