package wesleyfrederickoh.wesleyfrederickohUnity.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import wesleyfrederickoh.wesleyfrederickohUnity.model.Skill;

import java.util.Optional;
import java.util.UUID;

@Repository
public interface SkillRepository extends JpaRepository<Skill, UUID> {
    Optional<Skill> findByName(String name);
}
